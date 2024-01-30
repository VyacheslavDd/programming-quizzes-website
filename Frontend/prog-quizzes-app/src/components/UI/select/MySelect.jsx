

function MySelect({options, defaultValue, sortFieldValue, onChangeFunc}) {

    return(
        <>
            {options.length > 0
            ?
            <select value={sortFieldValue} onChange={(e) => onChangeFunc(e.target.value)}>
                <option value="" disabled>{defaultValue}</option>
                {options.map((option, index) => <option value={option.value} key={index}>{option.name}</option>)}
            </select>
            : <></>}
        </>
    )
}

export default MySelect