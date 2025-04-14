import CertificationItem from '../ui/CertificationItem';
import '../../styles/components/portfolio.css';

const Certification = ({ certificationData }) => {
    return (
        <section id="certifications" className="certificate-section container-fluid px-0">
            <div className="container">
                <h2 className="section-title">Certification</h2>


                {/* Certification Grid */}
                <div className="certificate-grid">
                    {certificationData.map((item, index) => (
                        <CertificationItem
                            key={index}
                            title={item.Title}
                            issuer={item.Issuer}
                            date={item.Date}
                            link={item.Url}
                        />
                    ))}
                </div>
            </div>
        </section>
    );
};

export default Certification;