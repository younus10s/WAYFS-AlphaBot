import Home from './Home';
import ControlRobot from './ControlRobot';
import { Route, Routes, Link } from 'react-router-dom';
import { Navbar, Nav } from 'react-bootstrap';

/**
 * Component to render a header for each page.
 * Used in App.jsx file to be rendered on every page in app.
 */
function Header() {
    return (
        <>
            <Navbar className="navbar fixed-top bg-secondary-color shadow-md">
                <Navbar.Brand to="/" className="navbar-brand"><i className="fa-solid fa-robot ml-4" style={{ color: "white" }}></i></Navbar.Brand>
                <Nav className='gap-4 text-white py-1 text-xl'>
                    <Link to="/" className="nav-link ml-2 text-white">Home</Link>
                    <Link to="/control" className="nav-link ml-2 text-white">Control Robot</Link>
                </Nav>
            </Navbar>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/control" element={<ControlRobot />} />
            </Routes>
        </>
    );
}

export default Header;
