import { Route } from "react-router-dom";


function App() {


    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Lista />} />
                <Route path="/nuevo" element={<Lista />} />
                <Route />
            </Routes>
        </BrowserRouter>
        
    )
    
    
    
}

export default App;