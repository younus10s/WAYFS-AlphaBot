import React from 'react';

const MovingStep = ({ commands }) => {



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