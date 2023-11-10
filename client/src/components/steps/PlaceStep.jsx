import React, { useState } from 'react';

const PlaceStep = ({ placeValues, handleXChange, handleYChange, handleDirChange }) => {


    return (<div>
        <h5 className='text-md m-8'>Before you start controlling the robot, you need to define where the robot should start from.
        </h5>
        <h5 className='text-md m-6'>Enter coordinates and direction:</h5>
        <div className='row flex justify-start'>
            <label className='col'>X coordinate: </label>
            <input className='col border-3 m-2 w-12' value={placeValues.xcoord} onChange={handleXChange}></input>
        </div>
        <div className='row flex justify-start'>
            <label className='col'>Y coordinate: </label>
            <input className='col border-3 m-2 w-12' value={placeValues.ycoord} onChange={handleYChange}></input>
        </div>
        <div className='row flex justify-start'>
            <label className='col'>Direction: </label>
            <select className='col border-3 m-2 w-18' value={placeValues.direction} onChange={handleDirChange}>
                <option>North</option>
                <option>South</option>
                <option>West</option>
                <option>East</option>
            </select>
        </div>
    </div>)
}

export default PlaceStep;