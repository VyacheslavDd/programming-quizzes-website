
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
    static quizDescriptionLength = 60;

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
}