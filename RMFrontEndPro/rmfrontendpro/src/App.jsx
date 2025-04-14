import { useState, useEffect, useContext } from 'react';
import { DataContext } from './context/DataContext';
import LoadingScreen from './assets/components/ui/LoadingScreen';
import ErrorScreen from './assets/components/ui/ErrorScreen';
import Navbar from './assets/components/common/Navbar';
import Header from './assets/components/common/Header';
import About from './assets/components/sections/ProfessionalSummary';
import Certifications from './assets/components/sections/Certification';
import Skills from './assets/components/sections/Skills';
import Portfolio from './assets/components/sections/Portfolio';
import Experience from './assets/components/sections/Experience';
import Contact from './assets/components/sections/Contact';
import Qualifications from './assets/components/sections/Qualifications';
import Footer from './assets/components/common/Footer';
import './assets/styles/main.css';

function App() {
    const [activeSection, setActiveSection] = useState('about');
    const getSocialLinks = (personalData) => {
        return Object.entries(personalData.SocialLinks).map(([index, link]) => (
            <a
                key={index}
                href={link}
                target="_blank"
                rel="noopener noreferrer"
            >
                <i className={`fab fa-${index}`}></i>
            </a>
        ));
    }

    useEffect(() => {
        const handleScroll = () => {
            const sections = ['about', 'skills', 'experience', 'qualifications', 'certifications', 'portfolio', 'contact'];
            const scrollPosition = window.scrollY + 100;

            for (const section of sections) {
                const element = document.getElementById(section);
               // console.log("element ofset ..  " + section + ". \n\nele height=" + element.offsetHeight + "= \tele offsettop = " + element.offsetTop + " element.offsetTop <= scrollPosition => " + (element.offsetTop <= scrollPosition) + "  (element.offsetTop + element.offsetHeight) > scrollPosition => " + ((element.offsetTop + element.offsetHeight) > scrollPosition));

                if (element && element.offsetTop <= scrollPosition &&
                    (element.offsetTop + element.offsetHeight) > scrollPosition) {
                    console.log(section);
                    setActiveSection(section);
                    break;
                }
            }
        };

        window.addEventListener('scroll', handleScroll);
        return () => window.removeEventListener('scroll', handleScroll);
    }, []);

    const { data, loading, error } = useContext(DataContext);

    if (loading) {
        return (<LoadingScreen></LoadingScreen>);
    }

    if (error)
        return <ErrorScreen message={error} />;

  
    return (
        <div className="App" >
            <Navbar activeSection={activeSection} getSocialLinks={getSocialLinks} />
            <Header personalData={data.personalData} getSocialLinks={getSocialLinks} />
            <main>
                <About personalData={data.personalData} getSocialLinks={getSocialLinks} />
                <Skills skills={data.skills} />
                <Experience experiences={data.experiences} />
                <Qualifications qualifications={data.qualifications} />
                <Certifications certificationData={data.certifications} />
                <Portfolio portfolioItems={data.portfolioItems} />
                <Contact personalData={data.personalData} getSocialLinks={getSocialLinks} />
            </main>
            <Footer personalData={data.personalData} getSocialLinks={getSocialLinks} />
        </div>

    );
}

export default App;