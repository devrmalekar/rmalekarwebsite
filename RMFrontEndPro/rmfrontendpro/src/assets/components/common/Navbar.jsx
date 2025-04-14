import { Link } from 'react-scroll';
import React, { useState } from 'react'; 
import '../../styles/components/navbar.css';



const Navbar = ({ activeSection }) => {
    const [isOpen, setIsOpen] = useState(false);
    const handleToggle = () => {
        setIsOpen(prev => { return !prev });
     
    };
    const handleNavLinkClick = () => {
        setIsOpen(false);
    };

    return (
        <nav className="navbar navbar-expand-lg navbar-dark fixed-top">
            <div className="container">
                <Link to="home" className="navbar-brand" smooth={true} duration={500}>
                    My Portfolio
                </Link>
                <button title="navigation" className="navbar-toggler" type="button" data-bs-toggle="collapse"
                    data-bs-target="#navbarNav"
                    aria-controls="navbarNav"
                    aria-expanded="false"
                    aria-label="Toggle navigation"
                    onClick={handleToggle}               >
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className={`collapse navbar-collapse ${isOpen? "show":""}`} id="navbarNav">
                    <ul className="navbar-nav ms-auto">
                        {['about', 'skills', 'experience', 'qualifications',  'certifications', 'portfolio','contact'].map((section) => (
                            <li className="nav-item" key={section}>
                                <Link
                                    to={section}
                                    className={`nav-link ${activeSection === section ? 'active' : ''}`}
                                    smooth={true}
                                    duration={500}
                                    onClick={handleNavLinkClick}
                                >
                                    {section.charAt(0).toUpperCase() + section.slice(1)}
                                </Link>
                            </li>
                        ))}
                    </ul>
                </div>
            </div>
        </nav>
    );
};

export default Navbar;