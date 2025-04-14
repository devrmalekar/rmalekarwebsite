import '../../styles/ui/LoadingScreen.css';

const LoadingScreen = () => {
    return (
    <div className="loading-screen">
        <div className="spinner"></div>
        <h2>Loading Rehman's Portfolio Website...</h2>
        <p>Please wait while the server loads the data</p>
    </div>
)};

export default LoadingScreen;