import React, { useState } from 'react'

export default function useSuccessTimeout(ms) {
  const [isShowingSuccess, setIsShowingSuccess] = useState(false);

  const doTimeout = () => {
    setIsShowingSuccess(true);
    let timeout = setTimeout(() => {
      setIsShowingSuccess(false);
    }, ms)
    return timeout;
  }

  return [isShowingSuccess, setIsShowingSuccess, doTimeout];
}
