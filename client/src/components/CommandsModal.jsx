import React, { useState } from 'react';

function CommandsModal() {


    const [commands, setCommands] = useState([]);

    const sendCommands = () => {

    };

    const addCommand = (com) => {
        setCommands(oldCommands => [...oldCommands, com]);
    };

    return (
        <div className="container">
            <div className='row p-2 bg-white self-start max-w-screen-md'>
                <div className='col m-6'>Add movements to robot and press "Send"</div>
                <div className='container'>
                    {commands.map((command, index) => (
                        <div className='row text-black' key={index}>
                            <p >{command}</p>
                        </div>
                    ))}
                </div>
            </div >

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
        </div >
    );
}

export default CommandsModal