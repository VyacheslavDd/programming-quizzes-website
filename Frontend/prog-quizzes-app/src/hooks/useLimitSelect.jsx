
import React, { useState } from 'react'

export default function useLimitSelect() {
    const [limitOptions, setLimitOptions] = useState([]);

    const setLimitSelect = () => {
        let options = [];
        let choices = [5, 10, 20, 30, 50, 100];
        for (let choice of choices) {
            options.push({id: choice, value: choice, text: choice});
        }
        setLimitOptions(options);
    }
    
    return [limitOptions, setLimitSelect];
}
