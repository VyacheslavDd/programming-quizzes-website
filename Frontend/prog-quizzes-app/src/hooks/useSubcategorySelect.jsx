
import React, { useState } from 'react'
import CategoryAPI from '../services/API/CategoryAPI';
import SubcategoryAPI from '../services/API/SubcategoryAPI';

export default function useSubcategorySelect(categoryOptions) {
    const [subcategoryOptions, setSubcategoryOptions] = useState([]);

    const prepareSubcategorySelect = async (selectedIndex=0) => {
        let subcategories = selectedIndex > 0 ?
        await CategoryAPI.GetConnectedSubcategoriesAsync(categoryOptions[selectedIndex].id) :
        await SubcategoryAPI.GetAllAsync();
        let options = [];
        for (let subcategory of subcategories) {
            options.push({id: subcategory.id, value: subcategory.id, text: subcategory.name})
        }
        setSubcategoryOptions([{id: 0, value: "", text: "Любая"}, ...options]);
    }

    return [subcategoryOptions, prepareSubcategorySelect];
}
