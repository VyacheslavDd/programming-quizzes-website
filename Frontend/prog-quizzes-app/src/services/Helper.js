
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
    static inputTextType = "text";
    static inputPasswordType = "password";
    static quizDescriptionLength = 60;
    static emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    static loginRegex = /^[A-Za-z][A-Za-z_0-9]{7,19}$/;
    static passwordRegex = /^[A-Za-z0-9@~!%&_]{8,15}$/;

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

    static updateFieldData(regexObject, input,  hintMessage, setHint, setIsCorrect) {
        let testPassed = regexObject.test(input);
        setHint(testPassed ? "" : hintMessage);
        setIsCorrect(testPassed ? true : false);
    }
}