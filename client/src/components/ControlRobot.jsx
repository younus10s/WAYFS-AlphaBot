import React, { useState } from 'react';
import Stepper from './Stepper';
import StepperControl from './StepperControl';
import PlaceStep from './steps/PlaceStep';
import CommandStep from './steps/CommandStep';
import StepperContext from '../contexts/StepperContext';
/**
 * Component to control robot by adding, removing and sending a string of commands.
 * Commands are sent by starting a websocket.
 */
function ControlRobot() {
    //const [connected, setConnected] = useState(false);
    const [currentStep, setCurrentStep] = useState(1);
    const [commands, setCommands] = useState([]);
    const [placeValues, setPlaceValues] = useState(
        {
            xcoord: 0,
            ycoord: 0,
            direction: "North"
        }
    );

    const steps = [
        "Placement of Robot",
        "Enter Commands"
    ];

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
        const fullCommands = combineCommands();

        console.log(fullCommands);
        const webSocket = new WebSocket('ws://localhost:5175');
        // When running server in localhost: ws://localhost:5175
        // When running server in RPi: ws://192.168.158.236:5175
        webSocket.onopen = () => {
            console.log('WebSocket connected');
            webSocket.send(fullCommands);
            console.log(fullCommands);
            setConnected(true);
        };

        webSocket.onerror = (error) => {
            console.error('WebSocket error: ' + error);
        };

        // webSocket.onmessage = (event) => {
        //     // Parse message from string into an array of doubles
        //     console.log(typeof event.data)
        //     const doubles = JSON.parse(event.data);
        // };

        webSocket.onclose = (event) => {
            if (event.wasClean) {
                console.log('WebSocket closed cleanly');
            } else {
                console.error('WebSocket connection abruptly closed');
            }
            console.log('Close code: ' + event.code + ', Reason: ' + event.reason);
        };
    };

    const combineCommands = () => {
        let fullCommands = "PLACE,";
        fullCommands += placeValues.xcoord.concat(",", placeValues.ycoord);
        fullCommands = fullCommands.concat(",", placeValues.direction.toUpperCase());


        fullCommands = fullCommands.concat(",", commands);
        fullCommands = fullCommands.concat(",", "REPORT"); // Final command to be added

        return fullCommands;
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
            default:
                return null;
        }
    }

    const handleClick = (direction) => {
        let newStep = currentStep;

        direction === "next" ? newStep++ : newStep--;
        // check if steps are withing bounds
        newStep > 0 && newStep <= steps.length && setCurrentStep(newStep);
    }

    return (
        <div>
            <div className="container horizontal mt-5">
                <div className='row p-2 bg-white self-start max-w-screen-md'>
                    {/* 
                    <Stepper steps={steps} currentStep={currentStep} /> */}
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