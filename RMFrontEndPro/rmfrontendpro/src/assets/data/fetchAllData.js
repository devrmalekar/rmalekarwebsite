
/*import personalData from './PersonalData.json';
import qualifications from './QualificationData.json';
import experiences from './ExperienceData.json';
import skills from './SkillData.json';
import portfolioItems from './PortfolioData.json';

const cache = {};
export const fetchAllData = async () => {
    if (cache.data) return cache.data;
    const data = {
        personalData,
        qualifications,
        experiences,
        skills,
        portfolioItems
    };
    cache.data = data;
    return data;
}*/
//const cache = {};
const fetchData = async (whichFile) => {

    try {
        
        const response = await fetch(`/data/${whichFile}.json?t=${Date.now()}`);
       // console.log(response);
        if (!response.ok) throw new Error('Network response was not ok');
        return await response.json();
    } catch (error) {
        let parts = whichFile.split(/(?=[A-Z])/);
       // console.log(parts);
        const requiredData = parts.map(
            part => part.charAt(0).toUpperCase() + part.slice(1)
        ).join(" ");
       // console.log(`Error fetching ${requiredData}:`, error);
        return await `Error fetching ${requiredData}\nError Message:`+error;
    }
};

export const fetchAllData = async () => {
    //if (cache.data) return cache.data;
    const [personalData, qualifications, experiences, skills, certifications, portfolioItems] = await Promise.all([
        fetchData("PersonalData"),
        fetchData("QualificationData"),
        fetchData("ExperienceData"),
        fetchData("SkillData"),
        fetchData("CertificationData"),
        fetchData("PortfolioData")
    ])
        
        .catch((error) => {
            console.log("One Failed. " + error);
        }); 

    var data = {
        personalData,
        qualifications,
        experiences,
        skills,
        certifications,
        portfolioItems
    };

   // cache.data = data;
    return data;
};