
import React, { useState } from 'react'

export default function useFormSubmit(initialValue, pendingValue, callback) {
  
    const [submitValue, setSubmitValue] = useState(initialValue);
    const [isPending, setIsPending] = useState(false);
    const [isSuccess, setIsSuccess] = useState(false);
    const [message, setMessage] = useState("");

    const submit = async () => {
        setIsSuccess(false);
        setSubmitValue(pendingValue);
        setIsPending(true);
        let submissionResult;
        try {
            submissionResult = await callback();
            let responseCode = submissionResult.responseCode; 
            if (responseCode >= 200 && responseCode <= 299) {
                setIsSuccess(true);
                setMessage("");
            }
            else {
                setIsSuccess(false);
                setMessage(submissionResult.errorMessage);
            }
        }
        catch {
            submissionResult = null;
            setIsSuccess(false);
            setMessage("Произошла ошибка на стороне сервера. Попробуйте позже");
        }
        setIsPending(false);
        setSubmitValue(initialValue);
        return submissionResult;
    }

    return [submitValue, isPending, isSuccess, message, submit]
}
