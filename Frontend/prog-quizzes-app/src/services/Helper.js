
export default class Helper {
    static difficulties = {
        1: "Легкая",
        2: "Средняя",
        3: "Тяжелая",
        4: "Эксперт"
    }

    static getDifficultyProperty(difficultyValue) {
        if (typeof difficultyValue === "number" && difficultyValue in Helper.difficulties) {
            return "★".repeat(difficultyValue) + " " + Helper.difficulties[difficultyValue];
        }
        return "Неизвестная"
    }
}