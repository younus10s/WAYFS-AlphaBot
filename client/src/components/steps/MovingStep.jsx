import React from 'react';

const MovingStep = ({ commands }) => {


    return (
        <div className='row flex justify-center'>
            <p className='mb-8 flex justify-center'>Robot is moving...</p>
            <div className='row flex justify-center'>
                {commands.map((cmd, index) => (
                    <p key={index} className='row flex justify-center'>{cmd}</p>
                ))}
            </div>
        </div>
    );

};

export default MovingStep;