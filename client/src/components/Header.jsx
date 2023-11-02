
import Home from './Home';
import ControlRobot from './ControlRobot';
import { Route, Routes, Link } from 'react-router-dom';
import { Navbar, Nav } from 'react-bootstrap';

function Header() {
    return (
        <>
            <Navbar className="navbar fixed-top bg-secondary-color shadow-md">
                {/* <Navbar.Brand to="/" className="navbar-brand">Logo</Navbar.Brand> */}
                <Nav className='gap-4 text-white py-1 text-xl'>
                    <Link to="/" className="nav-link text-white">Home</Link>
                    <Link to="/control" className="nav-link text-white">Control Robot</Link>
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
