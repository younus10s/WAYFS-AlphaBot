import React from 'react';

const MovingStep = ({ commands, currentCommand, i }) => {


    return (
        <div className='row flex justify-center'>
            <p className='mb-8 flex justify-center'>Robot is {currentCommand}</p>
            <div className='row flex justify-center'>
                {commands.map((cmd, index) => (
                    <p key={index} className={`row flex justify-center ${index == i && cmd === currentCommand ? 'currentCmd' : ''}`}>{cmd}</p>
                ))}
            </div>
        </div>
    );

};

export default MovingStep;