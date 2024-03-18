
import React, { useMemo } from 'react'
import Helper from '../services/Helper';

export function useFilterSorting(quizzes, difficulty, category, subcategory) {
  
  const includesSubcategory = (quizSubcategories) => {
    for (let quizSubcategory of quizSubcategories) {
      if (quizSubcategory.name === subcategory) {
        return true;
      }
    }
    return false;
  }

  const filteredQuizzes = useMemo(() => {
    let filteredByCategory = quizzes.filter(quiz => category === "" || quiz.categoryName === category);
    let filteredBySubcategory = filteredByCategory.filter(quiz => subcategory === "" || includesSubcategory(quiz.subcategories));
    let filteredByDifficulty = filteredBySubcategory.filter(quiz => Helper.difficulties[difficulty] === undefined || 
      quiz.difficulty === difficulty);
    return filteredByDifficulty;
  }, [quizzes, difficulty, category, subcategory])

  return filteredQuizzes;
}

export function useSearchSorting(quizzes, searchQuery) {
  const filteredQuizzes = useMemo(() => {
    let lowerQuery = searchQuery.toLowerCase();
    let filteredBySearch = quizzes.filter(quiz => searchQuery === "" || quiz.title.toLowerCase().includes(lowerQuery) ||
    quiz.description.toLowerCase().includes(lowerQuery));
    return filteredBySearch;
  }, [searchQuery, quizzes]);

  return filteredQuizzes;
}
