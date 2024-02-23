import React, { useState } from 'react'

export default function useFetching(callback, isLoadingInitial=false) {

    const [isLoading, setLoading] = useState(isLoadingInitial);
    const [isError, setIsError] = useState(false);

    async function fetch() {
        try {
            setLoading(true);
            await callback();
            setLoading(false);
        }
        catch {
            setLoading(false);
            setIsError(true);
        }
    }

    return [fetch, isLoading, isError];
}
