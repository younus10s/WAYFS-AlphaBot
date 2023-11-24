import React, { useEffect, useState } from 'react';
import StepperControl from './StepperControl';
import PlaceStep from './steps/PlaceStep';
import CommandStep from './steps/CommandStep';
import StepperContext from '../contexts/StepperContext';
import MovingStep from './steps/MovingStep';
/**
 * Component to control robot by adding, removing and sending a string of commands.
 * Commands are sent by starting a websocket.
 */
function ControlRobot() {
    const [currentStep, setCurrentStep] = useState(1);
    const [commands, setCommands] = useState([]);
    const [webSocket, setWebSocket] = useState(null);
    const [currentCommand, setCurrentCommand] = useState("IDLE");
    const [currentIndex, setCurrentIndex] = useState("0");

    const [placeValues, setPlaceValues] = useState(
        {
            xcoord: "0",
            ycoord: "0",
            direction: "North"
        }
    );

    const [gunnarPosition, setGunnarPosition] = useState({ x: 258, y: 250, deg: '0deg' });
    const [destPosition, setdestPosition] = useState({ x: 0, y: 0 });

    const moveGunnar = (gridX, gridY, dir) => {
        // Convert grid coordinates to pixel position
        const pixelX = gridX * 59 + 258;
        const pixelY =  250 - gridY * 59;
  
        var direction = '0deg';
        
        if(dir == "NORTH")
          direction = '-90deg';
        else if(dir == "WEST")
          direction = '-180deg';
        else if(dir == "SOUTH")
          direction = '-270deg';
        else if(dir == "EAST")
          direction = '0deg';
  
        // Update state with the new pixel position
        setGunnarPosition({ x: pixelX, y: pixelY, deg: direction });
      };

    const steps = [
        "Placement of Robot",
        "Enter Commands",
        "Show robot move"
    ];


    useEffect(() => {
        // Function to initialize WebSocket connection
        const connectWebSocket = () => {
          const newWebSocket = new WebSocket('ws://192.168.187.236:5175');
    
          newWebSocket.onopen = () => {
            console.log('Connected to WebSocket');
          };
    
          newWebSocket.onmessage = (event) => {
            console.log('Message from server ');

            const message = JSON.parse(event.data);
            if(message.Title == "status")
                setCurrentIndex(parseInt(message.Msg[0]))
                setCurrentCommand(message.Msg[1])
                moveGunnar(message.Msg[2], message.Msg[3], message.Msg[4])

            console.log(message)
          };
    
          setWebSocket(newWebSocket);
        };
    
        connectWebSocket();
    
        // Cleanup function to close WebSocket connection
        return () => {
          if (webSocket) {
            webSocket.close();
            console.log('WebSocket disconnected');
          }
        };
      }, []);
    


    

    const handleXChange = (event) => {
        setPlaceValues(prevState => {
            return { ...prevState, xcoord: event.target.value }
        });
    }

    const handleYChange = (event) => {
        setPlaceValues(prevState => {
            return { ...prevState, ycoord: event.target.value }
        });
    }

    const handleDirChange = (event) => {
        setPlaceValues(prevState => {
            return { ...prevState, direction: event.target.value }
        });
    }

    const handleSendClick = () => {

        handleClick("send"); // To update current step

        const placeString = `PLACE,${placeValues.xcoord},${placeValues.ycoord},${placeValues.direction.toUpperCase()}`;
        commands.unshift(placeString);
        commands.push("REPORT");
        
        const msg = {
            "Title": "commands",
            "Msg": commands
        }
        //const fullCommands = combineCommands();
        webSocket.send(JSON.stringify(msg));
        console.log("Sending:")
        console.log(msg)


    };


    const addCommand = (com) => {
        setCommands(oldCommands => [...oldCommands, com]);
    };


    const removeCommand = (indexToRemove) => {
        const updatedCommands = commands.filter((_, index) => index !== indexToRemove);
        setCommands(updatedCommands);
    };

    const displayStep = (step) => {
        switch (step) {
            case 1:
                return <PlaceStep
                    placeValues={placeValues}
                    handleXChange={handleXChange}
                    handleYChange={handleYChange}
                    handleDirChange={handleDirChange} />
            case 2:
                return <CommandStep
                    commands={commands}
                    addCommand={addCommand}
                    removeCommand={removeCommand}
                    placeValues={placeValues} />
            case 3:
                return <MovingStep
                    commands={commands}
                    currentCommand={currentCommand}
                    i={currentIndex} />
            default:
                return null;
        }
    }

    const handleClick = (direction) => {
        let newStep = currentStep;

        if(direction === "next")
            moveGunnar(placeValues.xcoord, placeValues.ycoord, placeValues.direction.toUpperCase());

        direction === "next" || direction === "send" ? newStep++ : newStep--;
        // check if steps are withing bounds
        newStep > 0 && newStep <= steps.length && setCurrentStep(newStep);

        console.log(currentStep);
    }

    const handleMoveClick = () => {
        // Example: Move to (100, 100) and rotate 45 degrees
        moveGunnar(2, 2, 'NORTH');
    };

    const handleCellClick = (x, y) => {
        // Example: Move to (100, 100) and rotate 45 degrees
        setdestPosition(x, y);
        console.log("(" + x + ":" + y + ")");
    };

    return (
        <div>
            <div className="container horizontal mt-5 relative">
                <img className="absolute w-8 h-8" style={{ left: `${gunnarPosition.x}px`, top: `${gunnarPosition.y}px`, transform: `rotate(${gunnarPosition.deg})` }} src="src/assets/pac.png" />
                <div className=' container row gap-0 self-start w-50'>
                    <div className='col p-0' onClick={() => handleCellClick(0,4)}>
                        <img src="src/assets/plus.png"/>
                    </div>
                    <div className='col p-0' onClick={() => handleCellClick(1,4)}>
                        <img src="src/assets/plus.png" />
                    </div>
                    <div className='col p-0' onClick={() => handleCellClick(2,4)}>
                        <img src="src/assets/plus.png"/>
                    </div>
                    <div className='col p-0' onClick={() => handleCellClick(3,4)}>
                        <img src="src/assets/plus.png" />
                    </div>
                    <div className='col p-0' onClick={() => handleCellClick(4,4)}>
                        <img src="src/assets/plus.png"/>
                    </div>
                </div>
                <div className=' container row gap-0 self-start w-50'>
                    <div className='col p-0' onClick={() => handleCellClick(0,3)}>
                        <img src="src/assets/plus.png"/>
                    </div>
                    <div className='col p-0' onClick={() => handleCellClick(1,3)}>
                        <img src="src/assets/plus.png" />
                    </div>
                    <div className='col p-0' onClick={() => handleCellClick(2,3)}>
                        <img src="src/assets/plus.png"/>
                    </div>
                    <div className='col p-0' onClick={() => handleCellClick(3,3)}>
                        <img src="src/assets/plus.png" />
                    </div>
                    <div className='col p-0' onClick={() => handleCellClick(4,3)}>
                        <img src="src/assets/plus.png"/>
                    </div>
                </div>
                <div className=' container row gap-0 self-start w-50'>
                    <div className='col p-0' onClick={() => handleCellClick(0,2)}>
                        <img src="src/assets/plus.png"/>
                    </div>
                    <div className='col p-0' onClick={() => handleCellClick(1,2)}>
                        <img src="src/assets/plus.png" />
                    </div>
                    <div className='col p-0' onClick={() => handleCellClick(2,2)}>
                        <img src="src/assets/plus.png"/>
                    </div>
                    <div className='col p-0' onClick={() => handleCellClick(3,2)}>
                        <img src="src/assets/plus.png" />
                    </div>
                    <div className='col p-0' onClick={() => handleCellClick(4,2)}>
                        <img src="src/assets/plus.png"/>
                    </div>
                </div>
                <div className=' container row gap-0 self-start w-50'>
                    <div className='col p-0' onClick={() => handleCellClick(0,1)}>
                        <img src="src/assets/plus.png"/>
                    </div>
                    <div className='col p-0' onClick={() => handleCellClick(1,1)}>
                        <img src="src/assets/plus.png" />
                    </div>
                    <div className='col p-0' onClick={() => handleCellClick(2,1)}>
                        <img src="src/assets/plus.png"/>
                    </div>
                    <div className='col p-0' onClick={() => handleCellClick(3,1)}>
                        <img src="src/assets/plus.png" />
                    </div>
                    <div className='col p-0' onClick={() => handleCellClick(4,1)}>
                        <img src="src/assets/plus.png"/>
                    </div>
                </div>
                <div className=' container row gap-0 self-start w-50'>
                    <div className='col p-0' onClick={() => handleCellClick(0,0)}>
                        <img src="src/assets/plus.png"/>
                    </div>
                    <div className='col p-0' onClick={() => handleCellClick(1,0)}>
                        <img src="src/assets/plus.png" />
                    </div>
                    <div className='col p-0' onClick={() => handleCellClick(2,0)}>
                        <img src="src/assets/plus.png"/>
                    </div>
                    <div className='col p-0' onClick={() => handleCellClick(3,0)}>
                        <img src="src/assets/plus.png" />
                    </div>
                    <div className='col p-0' onClick={() => handleCellClick(4,0)}>
                        <img src="src/assets/plus.png"/>
                    </div>
                </div>
            </div>

            <div className="container horizontal mt-3">
            <button onClick={handleMoveClick}>Move and Rotate</button>
                <div className='row p-2 m-2 bg-white self-start max-w-screen-md'>
                    <div className='text-3xl col m-3 font-semibold'>Step {currentStep}</div>
                    <div className='text-xl'>
                        <StepperContext.Provider value={{}}>
                            {displayStep(currentStep)}
                        </StepperContext.Provider>
                    </div>
                </div>
            </div>

            <StepperControl
                handleClick={handleClick}
                onSendClick={handleSendClick}
                currentStep={currentStep}
                steps={steps}
            />
        </div>
    );
}

export default ControlRobot;