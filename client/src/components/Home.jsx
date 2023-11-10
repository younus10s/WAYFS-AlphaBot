import React, { useEffect } from 'react';
import { Route, Routes, Link } from 'react-router-dom';
/**
 * Component to render start/home page for app.
 */
function Home() {

    return (
        <div className='row'>
            <div>
                <div className="container">
                    <div className='p-10 m-18 bg-white self-start max-w-screen-md'>
                        <h3 className='text-4xl p-4 mb-2'>Hello!</h3>
                        <i className="fa-solid fa-robot text-4xl mb-4" style={{ color: "blue" }}></i>
                        <h5 className='text-md p-8'>Welcome to our Robot lab website.</h5>
                        <h5 className='text-md p-8'>Press
                            <Link to="/control" className='text-blue-500'> here </Link>
                            to start controlling the robot.</h5>
                    </div>
                </div>
            </div>
        </div>

    );
}

export default Home;