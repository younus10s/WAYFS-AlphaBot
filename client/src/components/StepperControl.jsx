import React from 'react';

const StepperControl = ({ handleClick, onSendClick, currentStep, steps }) => {
    return (
        <div>
            {currentStep <= steps.length - 1 ?
                <div className='container flex justify-around mt-4 mb-8'>
                    {/* Back button */}

                    <button
                        onClick={() => handleClick("back")}
                        className={`bg-gray-100  text-slate-600 uppercase py-2 px-4 rounded-xl font-semibold 
        border-2 border-slate-300 hover:bg-slate-700 hover:border-slate-700 transition duration-200 ease-in-out 
        ${currentStep === 1 ? "opacity-50 cursor-not-allowed" : ""
                            } cursor-pointer`}>
                        Back
                    </button>

                    {currentStep === steps.length - 2 ?
                        (<div>
                            <button
                                onClick={() => handleClick("next")}
                                className='text-slate-600 bg-gray-100 uppercase py-2 px-4 rounded-xl font-semibold 
        cursor-pointer border-2 border-slate-300 hover:bg-slate-700 hover:border-slate-700  transition duration-200 ease-in-out'>
                                Next
                            </button>
                        </div>) :
                        (<div>
                            <button
                                onClick={() => onSendClick()}
                                className='text-slate-600 bg-gray-100 uppercase py-2 px-4 rounded-xl font-semibold 
        cursor-pointer border-2 border-slate-300 hover:bg-slate-700 hover:border-slate-700  transition duration-200 ease-in-out'>
                                Send
                            </button>
                        </div>)}
                </div>
                :
                <div>
                </div>
            }

            { /* Next button */}
        </div>);
};

export default StepperControl;