
import React, { useState } from 'react'

export default function useDateSortingSelect() {
  const [dateOptions,  setDateOptions] = useState([]);

  const setDateSortingSelect = () => {
    let options = [{id: 1, value: "creationDate", text: "Сначала новые"},
                    {id: 2, value: "-creationDate", text: "Сначала старые"}]
    setDateOptions(options);
  }

  return [dateOptions, setDateSortingSelect];
}
