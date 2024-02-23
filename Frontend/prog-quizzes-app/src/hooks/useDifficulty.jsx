
import React, { useState } from 'react'

export default function useDifficulty(callback) {
  const [difficulty, setDifficulty] = useState("");
  function parseDifficulty() {
    let difficultyString = callback();
    setDifficulty(difficultyString);
  }

  return [parseDifficulty, difficulty];
}
