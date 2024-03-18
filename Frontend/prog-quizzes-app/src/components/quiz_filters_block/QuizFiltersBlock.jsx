import CategoryAPI from "../../services/API/CategoryAPI"
import SubcategoryAPI from "../../services/API/SubcategoryAPI"
import Helper from "../../services/Helper"
import OptionsSelect from "../options_select/OptionsSelect"
import SearchInput from "../search_input/SearchInput"
import styles from "./QuizFiltersBlock.module.css"
import React, { useEffect, useRef, useState } from 'react'

export default function QuizFiltersBlock({setSearchQuery, setDifficulty, setCategory, setSubcategory}) {

    const onSearchInputAction = (query) => {
        setSearchQuery(query);
    }

    const onDifficultySelect = (e) => {
        setDifficulty(Number(e.target.value));
    }

    const onCategorySelect = async (e) => {
        setCategory(e.target.value);
        await prepareSubcategorySelect(e.target.selectedIndex);
        setSubcategory("");
    }

    const onSubcategorySelect = (e) => {
        setSubcategory(e.target.value);
    }

    const [difficultyOptions, setDifficultyOptions] = useState([{id: 0, value: 0, text: "Любая"}]);
    const [categoryOptions, setCategoryOptions] = useState([{id: 0, value: "", text: "Любая"}]);
    const [subcategoryOptions, setSubcategoryOptions] = useState([{id: 0, value: "", text: "Любая"}]);
    
    const prepareDifficultySelect = () => {
        setDifficultyOptions(prev => [{id: 0, value: 0, text: "Любая"}]);
        for (let key of Object.keys(Helper.difficulties)) {
            let text = Helper.difficulties[key]
            setDifficultyOptions(prev => [...prev, {id: key, value: key, text: text}])
        }
    }

    const prepareCategorySelect = async () => {
        let categories = await CategoryAPI.GetAllAsync();
        let options = [];
        for (let category of categories) {
            options.push({id: category.id, value: category.name, text: category.name})
        }
        setCategoryOptions([{id: 0, value: "", text: "Любая"}, ...options]);
    }

    const prepareSubcategorySelect = async (selectedIndex=0) => {
        let subcategories = selectedIndex > 0 ?
        await CategoryAPI.GetConnectedSubcategoriesAsync(categoryOptions[selectedIndex].id) :
        await SubcategoryAPI.GetAllAsync();
        let options = [];
        for (let subcategory of subcategories) {
            options.push({id: subcategory.id, value: subcategory.name, text: subcategory.name})
        }
        setSubcategoryOptions([{id: 0, value: "", text: "Любая"}, ...options]);
    }

    useEffect(() => {
        async function asynchronousPreparations() {
            await prepareCategorySelect();
            await prepareSubcategorySelect();
        }
        prepareDifficultySelect();
        asynchronousPreparations();
    }, [])

    return (
        <div className={styles.filtersBlock}>
            <div className={styles.searchInput}>
                <SearchInput placeholder="Поиск по названию или описанию..." onInput={onSearchInputAction}/>
            </div>
            <div className={styles.filters}>
                <div>
                    <OptionsSelect label="Категория:" options={categoryOptions} onChange={onCategorySelect}/>
                    <OptionsSelect label="Подкатегория:" options={subcategoryOptions} onChange={onSubcategorySelect}/>
                </div>
                <div>
                    <OptionsSelect label="Сложность:" options={difficultyOptions} onChange={onDifficultySelect}/>
                    <OptionsSelect label="Лимит на страницу:" options={[{id: 1, value: "xd", text: "what"}, {id: 2, value: "xd", text: "whatt"}]}/>
                </div>
            </div>
        </div>
  )
}
