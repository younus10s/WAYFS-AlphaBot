import React, { useState } from 'react';

function ControlRobot() {
    const [connected, setConnected] = useState(false);
    const [commands, setCommands] = useState([]);

    const sendCommands = () => {

        const webSocket = new WebSocket('ws://localhost:5175');

        webSocket.onopen = () => {
            console.log('WebSocket connected');
            webSocket.send(commands);
            setConnected(true);
        };

        webSocket.onerror = (error) => {
            console.error('WebSocket error: ' + error);
        };

        console.log(commands);

        // webSocket.onmessage = (event) => {
        //     // Parse message from string into an array of doubles
        //     console.log(typeof event.data)
        //     const doubles = JSON.parse(event.data);

        //     setSentData(doubles);
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

    const addCommand = (com) => {
        setCommands(oldCommands => [...oldCommands, com]);
    };


    const removeCommand = (indexToRemove) => {
        const updatedCommands = commands.filter((_, index) => index !== indexToRemove);
        setCommands(updatedCommands);
    };

    return (
        <div>
            <div className="btn-group" role="group" aria-label="Default button group">
                <div className="container">
                    <div className='row p-2 bg-white self-start max-w-screen-md'>
                        <div className='col m-6'>Add movements to robot and press "Send"</div>
                        <div className='container'>
                            <ul >
                                {commands.map((command, index) => (
                                    <li className='row text-black' key={index}>
                                        <span className="icon">{command}
                                            <i onClick={() => removeCommand(index)} className="fa-sharp fa-solid fa-xmark m-2 text-red-600 hover:cursor-pointer"></i>
                                        </span>
                                    </li>
                                ))}

                            </ul>
                        </div>
                    </div>
                    <div className='row m-10'>
                        <div className='col'>
                            <button type="button" onClick={() => addCommand("Left")} className="btn bg-secondary-color text-white m-1 hover:bg-slate-700">Left</button>
                            <button type="button" onClick={() => addCommand("Forward")} className="btn bg-secondary-color text-white m-1  hover:bg-slate-700">Forward</button>
                            <button type="button" onClick={() => addCommand("Right")} className="btn bg-secondary-color text-white m-1 hover:bg-slate-700">Right</button>
                        </div>
                    </div>
                    <div className='row'>
                        <div className='col'>
                            <button type="button" onClick={sendCommands} className="btn py-3 p-8 bg-dark text-white m-2 hover:bg-slate-700">Send</button>
                        </div>
                    </div>
                </div>
            </div>
        </div >
    );
}

export default ControlRobot;