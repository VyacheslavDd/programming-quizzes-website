
import React, { useState } from 'react'
import Helper from '../services/Helper';

export default function useDifficultySelect() {

    const [difficultyOptions, setDifficultyOptions] = useState([]);

    const prepareDifficultySelect = () => {
        setDifficultyOptions(prev => [{id: 0, value: 0, text: "Любая"}]);
        for (let key of Object.keys(Helper.difficulties)) {
            let text = Helper.difficulties[key]
            setDifficultyOptions(prev => [...prev, {id: key, value: key, text: text}])
        }
    }

    return [difficultyOptions, prepareDifficultySelect];
}
