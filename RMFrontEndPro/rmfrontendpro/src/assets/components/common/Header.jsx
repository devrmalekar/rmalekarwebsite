
import '../../styles/components/header.css';


const Header = ({ personalData, getSocialLinks }) => {
    return (
        <header id="home" className="header-section container-fluid px-0">
            <div className="header-container container h-100">
                <div className="row h-100 align-items-center">
                    <div className="col-lg-6">
                        <h1 className="display-3">{personalData.FirstName} {personalData.LastName}</h1>
                        <p className="lead">{personalData.Title}</p>
                        <div className="social-links">
                            {
                                getSocialLinks(personalData)
                            }
                            
                        </div>
                    </div>
                    <div className="col-lg-6">
                        <img
                            src="/images/2999AC0320264325022_P.jpg"
                            alt="Profile"
                            className="img-fluid rounded-circle profile-image"
                        />
                    </div>
                </div>
            </div>
        </header>
    );
};

export default Header;