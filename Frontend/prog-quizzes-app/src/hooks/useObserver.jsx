import React, { useEffect, useRef } from 'react'

export default function useObserver(onSight, lastItemRef) {
    const observer = useRef();
    useEffect(() => {
        if (observer.current) {
            observer.current.disconnect();
        }
        observer.current = new IntersectionObserver(onSight);
        if (lastItemRef.current) {
            observer.current.observe(lastItemRef.current);
        }
    }, [lastItemRef]);
    return observer;
}
