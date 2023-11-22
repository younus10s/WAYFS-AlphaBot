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

    const [placeValues, setPlaceValues] = useState(
        {
            xcoord: "0",
            ycoord: "0",
            direction: "North"
        }
    );

    const steps = [
        "Placement of Robot",
        "Enter Commands",
        "Show robot move"
    ];


    useEffect(() => {
        // Function to initialize WebSocket connection
        const connectWebSocket = () => {
          const newWebSocket = new WebSocket('ws://localhost:5175');
    
          newWebSocket.onopen = () => {
            console.log('Connected to WebSocket');
          };
    
          newWebSocket.onmessage = (event) => {
            console.log('Message from server ');

            message = JSON.parse(event.data);
            if(message.Title == "status")
                setCurrentCommand(message.Msg[0])

            console.log(JSON.parse(event.data))
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
                    currentCommand={currentCommand} />
            default:
                return null;
        }
    }

    const handleClick = (direction) => {
        let newStep = currentStep;

        direction === "next" || direction === "send" ? newStep++ : newStep--;
        // check if steps are withing bounds
        newStep > 0 && newStep <= steps.length && setCurrentStep(newStep);

        console.log(currentStep);
    }

    return (
        <div>
            <div className="container horizontal mt-5">
                <div className='row p-2 m-4 bg-white self-start max-w-screen-md'>
                    <div className='text-3xl col m-6 font-semibold'>Step {currentStep}</div>
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