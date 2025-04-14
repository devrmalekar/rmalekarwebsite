import '../../styles/components/footer.css';

const Footer = ({ personalData }) => {
    return (
        <footer className="footer py-4 container-fluid px-0">
            <div className="container">
                <div className="footer-content">
                    <div className="col-md-12">
                        <p className="copyright">
                            &copy; {new Date().getFullYear()} {personalData.FirstName} {personalData.LastName}. All rights reserved.
                            </p>
                    </div>
                </div>
            </div>
        </footer>
    );
};

export default Footer;