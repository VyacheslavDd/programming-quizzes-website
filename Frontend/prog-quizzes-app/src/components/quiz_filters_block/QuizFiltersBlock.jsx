import useCategorySelect from "../../hooks/useCategorySelect"
import useSortingSelect from "../../hooks/useSortingSelect"
import useDifficultySelect from "../../hooks/useDifficultySelect"
import useLimitSelect from "../../hooks/useLimitSelect"
import useSubcategorySelect from "../../hooks/useSubcategorySelect"
import CategoryAPI from "../../services/API/CategoryAPI"
import SubcategoryAPI from "../../services/API/SubcategoryAPI"
import Helper from "../../services/Helper"
import GenericButton from "../UI/buttons/generic_button/GenericButton"
import OptionsSelect from "../UI/options_select/OptionsSelect"
import SearchInput from "../UI/search_input/SearchInput"
import styles from "./QuizFiltersBlock.module.css"
import React, { useEffect, useRef, useState } from 'react'

export default function QuizFiltersBlock({setSearchQuery, setDifficulty, setCategory, setSubcategory,
    setLimit, setParameterSort, onAccept}) {

    const onSearchInputAction = (query) => {
        setSearchQuery(query);
    }

    const onDifficultySelect = (e) => {
        let value = Number(e.target.value)
        setDifficulty(value > 0 ? value : null);
    }

    const onCategorySelect = async (e) => {
        let value = e.target.value;
        setCategory(value === "" ? null : value);
        setSubcategory(null);
        subCategoryRef.current.selectedIndex = 0;
        await setSubcategoryOptions(e.target.selectedIndex);
    }

    const onSubcategorySelect = (e) => {
        let value = e.target.value;
        setSubcategory(value === "" ? null : value);
    }

    const onParameterSortSelect = (e) => {
        setParameterSort(e.target.value);
    }

    const onLimitSelect = (e) => {
        setLimit(Number(e.target.value));
    }

    const subCategoryRef = useRef();
    const [difficultyOptions, setDifficultyOptions] = useDifficultySelect();
    const [categoryOptions, setCategoryOptions] = useCategorySelect();
    const [subcategoryOptions, setSubcategoryOptions] = useSubcategorySelect(categoryOptions);
    const [sortOptions, setSortOptions] = useSortingSelect();
    const [limitOptions, setLimitOptions] = useLimitSelect();


    useEffect(() => {
        async function asynchronousPreparations() {
            await setCategoryOptions();
            await setSubcategoryOptions();
        }
        setDifficultyOptions();
        setSortOptions();
        setLimitOptions();
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
                    <OptionsSelect label="Лимит на страницу:" options={limitOptions} onChange={onLimitSelect}/>
                </div>
            </div>
            <div className={styles.acceptFilters}>
                <GenericButton onClick={() => onAccept()}>Применить</GenericButton>
            </div>
            <div className={styles.dateSort}>
                <OptionsSelect label="Сортировать по:" options={sortOptions} onChange={onParameterSortSelect}/>
            </div>
        </div>
  )
}
