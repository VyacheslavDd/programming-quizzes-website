import { jwtDecode } from "jwt-decode";
import TokensAPI from "./API/TokensAPI";

export default class Helper {
    static difficulties = {
        1: "Легкая",
        2: "Средняя",
        3: "Тяжелая",
        4: "Эксперт"
    }
    static questionTypes = {
        1: "radio",
        2: "checkbox"
    }

    static rateOptions = [
        {id: 1, text: "Без оценки", value: 0},
        {id: 2, text: "Ужасно", value: 1},
        {id: 3, text: "Плохо", value: 2},
        {id: 4, text: "Удовлетворительно", value: 3},
        {id: 5, text: "Хорошо", value: 4},
        {id: 6, text: "Отлично", value: 5},
    ]

    static inputTextType = "text";
    static inputPasswordType = "password";
    static inputNumberType = "number";
    static inputDateType = "date";
    static quizDescriptionLength = 60;
    static emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    static loginRegex = /^[A-Za-z][A-Za-z_0-9]{7,19}$/;
    static phoneRegex = /^8[0-9]{10}$/
    static passwordRegex = /^[A-Za-z0-9@~!%&_]{8,15}$/;
    static nameRegex = /^[a-zA-Zа-яА-Я]{2,15}$/;
    static surnameRegex = /^[a-zA-Zа-яА-Я]{2,20}$/;
    static dateRegex = /^[0-9]{4}-[0-9]{2}-[0-9]{2}/;

    static getDifficultyProperty(difficultyValue) {
        if (typeof difficultyValue === "number" && difficultyValue in Helper.difficulties) {
            return "★".repeat(difficultyValue) + " " + Helper.difficulties[difficultyValue];
        }
        return "Неизвестная"
    }
    static getInputType(questionType) {
        if (typeof questionType === "number" && questionType in Helper.questionTypes) {
            return Helper.questionTypes[questionType];
        }
        return Helper.questionTypes[1];
    }

    static shuffleArray(array) {
        let currentIndex = array.length, randomIndex;
        while (currentIndex > 0) {
            randomIndex = Math.floor(Math.random() * currentIndex);
            currentIndex--;
            [array[currentIndex], array[randomIndex]] = [
            array[randomIndex], array[currentIndex]];
        }
        return array;
    }

    static shortenQuizDescription(description) {
        if (description.length <= Helper.quizDescriptionLength) {
            return description;
        }
        return description.slice(0, Helper.quizDescriptionLength) + "...";
    }

    static defineQuizResumeColor(correctAnswersCount, count) {
        let percent = correctAnswersCount / 2 * 100 / count;
        if (percent < 50) {
            return "#e84c55";
        }
        else if (percent < 80) {
            return "#98780d"
        }
        else {
            return "#10871a"
        }
    }

    static updateFieldData(regexObject, input,  hintMessage, setHint, correctPropertyName, setIsCorrect) {
        let testPassed = regexObject.test(input);
        setHint(testPassed ? "" : hintMessage);
        setIsCorrect((prev) => ({...prev, [correctPropertyName]: testPassed ? true : false}));
    }
}