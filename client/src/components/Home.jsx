import React, { useEffect, useState } from 'react';
import { Route, Routes, Link } from 'react-router-dom';
/**
 * Component to render start/home page for app.
 */
function Home() {
    const [ipAddress, setipAddress] = useState("");

    return (
        <div className='row'>
            <div>
                <div className="container">
                    <div className='p-10 m-18 bg-white self-start max-w-screen-md'>
                        <h3 className='text-4xl p-4 mb-2'>Hello!</h3>
                        <i className="fa-solid fa-robot text-4xl mb-4" style={{ color: "blue" }}></i>
                        <h5 className='text-md p-8'>Welcome to our Robot lab website.</h5>

                        <input 
                            className='bg-white border border-gray-300'
                            type="text" 
                            value={ipAddress} 
                            onChange={(e) => {
                                setipAddress(e.target.value);
                            }}
                            placeholder="Type IP-address:Port"
                        />

                        <h5 className='text-md p-8'>Press
                        <Link to={`/control/${ipAddress}`}> Here </Link>
                            to start controlling the robot.</h5>
                    </div>
                </div>
            </div>
        </div>

    );
}

export default Home;