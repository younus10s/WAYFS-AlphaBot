import React, { useState } from 'react';

const CommandStep = ({ commands, addCommand, removeCommand, placeValues }) => {


    return (<div>
        <div>
            <h4 className='italic mb-4 text-md'>Selected placement for robot: {placeValues.xcoord}, {placeValues.ycoord}, {placeValues.direction} </h4>
            <h5 className='p-2 mb-4 text-md'>Enter commands which the robot should execute.</h5>
            <div className='col'>
                <button type="button" onClick={() => addCommand("LEFT")} className="bg-gray-100  text-slate-600 uppercase py-2 px-6 m-2 rounded-xl font-semibold 
        border-2 border-slate-300 hover:bg-slate-700 hover:border-slate-700 transition duration-200 ease-in-out">Left</button>
                <button type="button" onClick={() => addCommand("MOVE")} className="bg-gray-100  text-slate-600 uppercase py-2 px-4 m-2 rounded-xl font-semibold 
        border-2 border-slate-300 hover:bg-slate-700 hover:border-slate-700 transition duration-200 ease-in-out">Move</button>
                <button type="button" onClick={() => addCommand("RIGHT")} className="bg-gray-100  text-slate-600 uppercase py-2 px-2 m-2 rounded-xl font-semibold 
        border-2 border-slate-300 hover:bg-slate-700 hover:border-slate-700 transition duration-200 ease-in-out">Right</button>
            </div>
        </div>
        <div className='container bg-slate-100'>
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

        {/* <div className='row'>
            <div className='col'>
                <button type="button" onClick={sendCommands} className="btn py-3 p-8 bg-dark text-white m-2 hover:bg-slate-700">Send</button>
            </div>
        </div> */}
    </div>);
};

export default CommandStep;