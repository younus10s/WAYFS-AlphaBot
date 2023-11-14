import React from 'react';

const MovingStep = ({ commands }) => {


    const ReceiveMessage = () => {

        webSocket.onclose = (event) => {
            if (event.wasClean) {
                console.log('WebSocket closed cleanly');
            } else {
                console.error('WebSocket connection abruptly closed');
            }
            console.log('Close code: ' + event.code + ', Reason: ' + event.reason);
        };
    };


    return (
        <div className='row'>
            <p className='mb-8'>Robot is moving...</p>
            <p>{commands}</p>
            <div className='row justify-center'>
                {commands.map((cmd, index) => {
                    <p className='row'>{cmd}</p>
                })}
            </div>
        </div>
    );

};

export default MovingStep;