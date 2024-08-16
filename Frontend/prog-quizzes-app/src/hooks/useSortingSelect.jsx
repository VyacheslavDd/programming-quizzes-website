
import React, { useState } from 'react'

export default function useSortingSelect() {
  const [options,  setOptions] = useState([]);

  const setSortingSelect = () => {
    let options = [{id: 1, value: "creationDate", text: "дате (сначала новые)"},
                    {id: 2, value: "-creationDate", text: "дате (сначала старые)"},
                    {id: 3, value: "rating", text: "рейтингу"}]
    setOptions(options);
  }

  return [options, setSortingSelect];
}
