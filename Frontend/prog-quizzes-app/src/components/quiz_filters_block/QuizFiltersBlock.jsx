import useCategorySelect from "../../hooks/useCategorySelect"
import useDateSortingSelect from "../../hooks/useDateSortingSelect"
import useDifficultySelect from "../../hooks/useDifficultySelect"
import useSubcategorySelect from "../../hooks/useSubcategorySelect"
import CategoryAPI from "../../services/API/CategoryAPI"
import SubcategoryAPI from "../../services/API/SubcategoryAPI"
import Helper from "../../services/Helper"
import OptionsSelect from "../UI/options_select/OptionsSelect"
import SearchInput from "../UI/search_input/SearchInput"
import styles from "./QuizFiltersBlock.module.css"
import React, { useEffect, useRef, useState } from 'react'

export default function QuizFiltersBlock({setSearchQuery, setDifficulty, setCategory, setSubcategory, setDateParameterSort}) {

    const onSearchInputAction = (query) => {
        setSearchQuery(query);
    }

    const onDifficultySelect = (e) => {
        setDifficulty(Number(e.target.value));
    }

    const onCategorySelect = async (e) => {
        setCategory(e.target.value);
        setSubcategory("");
        subCategoryRef.current.selectedIndex = 0;
        await setSubcategoryOptions(e.target.selectedIndex);
    }

    const onSubcategorySelect = (e) => {
        setSubcategory(e.target.value);
    }

    const onDateParameterSortSelect = (e) => {
        setDateParameterSort(e.target.value);
    }

    const subCategoryRef = useRef();
    const [difficultyOptions, setDifficultyOptions] = useDifficultySelect();
    const [categoryOptions, setCategoryOptions] = useCategorySelect();
    const [subcategoryOptions, setSubcategoryOptions] = useSubcategorySelect(categoryOptions);
    const [dateSortOptions, setDateSortOptions] = useDateSortingSelect();


    useEffect(() => {
        async function asynchronousPreparations() {
            await setCategoryOptions();
            await setSubcategoryOptions();
        }
        setDifficultyOptions();
        setDateSortOptions();
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
                    <OptionsSelect ref={subCategoryRef} label="Подкатегория:" options={subcategoryOptions} onChange={onSubcategorySelect}/>
                </div>
                <div>
                    <OptionsSelect label="Сложность:" options={difficultyOptions} onChange={onDifficultySelect}/>
                    <OptionsSelect label="Лимит на страницу:" options={[{id: 1, value: "xd", text: "what"}, {id: 2, value: "xd", text: "whatt"}]}/>
                </div>
            </div>
            <div className={styles.dateSort}>
                <OptionsSelect label="Сортировать по дате:" options={dateSortOptions} onChange={onDateParameterSortSelect}/>
            </div>
        </div>
  )
}
