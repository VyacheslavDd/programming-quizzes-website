
import React, { useState } from 'react'
import CategoryAPI from '../services/API/CategoryAPI';

export default function useCategorySelect() {
    const [categoryOptions, setCategoryOptions] = useState([]);

    const prepareCategorySelect = async () => {
        let categories = await CategoryAPI.GetAllAsync();
        let options = [];
        for (let category of categories) {
            options.push({id: category.id, value: category.name, text: category.name})
        }
        setCategoryOptions([{id: 0, value: "", text: "Любая"}, ...options]);
    }

    return [categoryOptions, prepareCategorySelect];
}
