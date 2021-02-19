


export const updateObjectInArray = (items, itemsID, objPropName, newObjprops) =>
{
    items.map(u => {
        if (u["objPropName"] === itemsID) {
            return {...u, ...newObjprops}

        }
        return u;
    })
}