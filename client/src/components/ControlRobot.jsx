import React, { useEffect, useState } from 'react';

function ControlRobot() {
    const [sentData, setSentData] = useState([]);
    const [connected, setConnected] = useState(false);
    const [buttonClickValue, setButtonClickValue] = useState(null);

    const Connect = (id) => {
        const webSocket = new WebSocket('ws://localhost:5175');

        setButtonClickValue(id);

        // webSocket.send("Hello server!");

        webSocket.onopen = () => {
            console.log('WebSocket connected');
            setConnected(true);
        };

        webSocket.onmessage = (event) => {
            // Parse message from string into an array of doubles
            console.log(typeof event.data)
            const doubles = JSON.parse(event.data);

            setSentData(doubles);
        };


        webSocket.onclose = (event) => {
            if (event.wasClean) {
                console.log('WebSocket closed cleanly');
            } else {
                console.error('WebSocket connection abruptly closed');
            }
            console.log('Close code: ' + event.code + ', Reason: ' + event.reason);
        };

        webSocket.onerror = (error) => {
            console.error('WebSocket error: ' + error);
        };

    };

    return (
        <div>
            <div className="btn-group" role="group" aria-label="Default button group">
                <button className="btn btn-dark button-style" onClick={() => Connect(1)}>Click here to make robot follow line</button>
                <button className="btn btn-dark button-style" onClick={() => Connect(2)}>Click here to control robot</button>
            </div>
            {buttonClickValue === 1 &&
                <><p>first value: {sentData[0]}, second value: {sentData[1]}, third value: {sentData[2]}</p>
                </>
            }
            <div className='row'>
                {buttonClickValue === 2 && ControlButtons()}
            </div>
        </div>
    );
}

function ControlButtons() {


    return (
        <div className="btn-group" role="group" aria-label="Default button group">
            <button type="button" className="btn btn-dark">Left</button>
            <button type="button" className="btn btn-dark">Forward</button>
            <button type="button" className="btn btn-dark">Right</button>
        </div>
    );
}

export default ControlRobot;