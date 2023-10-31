
import Home from './Home';
import ControlRobot from './ControlRobot';
import { Route, Routes, Link } from 'react-router-dom';
import { Navbar, Nav } from 'react-bootstrap';

function Header() {
    return (
        <>
            <Navbar className="navbar navbar-dark bg-dark fixed-top">
                <Navbar.Brand to="/" className="navbar-brand">Logo</Navbar.Brand>
                <Nav>
                    <Link to="/" className="nav-link">Home</Link>
                    <Link to="/control" className="nav-link">Control Robot</Link>
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
