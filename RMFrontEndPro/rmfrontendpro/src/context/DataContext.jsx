import { createContext, useEffect, useState } from 'react';
import { fetchAllData } from '../assets/data/fetchAllData';

export const DataContext = createContext();

export const DataProvider = ({ children }) => {
    const [data, setData] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const loadData = async () => {
            try {
                const data = await fetchAllData();
                setData(data);
            }
            catch (error) {
              //  console.log("we are here............");
                console.log(error.message);
                setError(error.message);
            }
            finally {
                setLoading(false);
            }
        };
        loadData();
    }, []);

    return (
        <DataContext.Provider value={{ data, loading, error }}>
            {children}
        </DataContext.Provider>
    );
};