/*===============================================
                    Site
===============================================*/

/*=============================
           Variables
=============================*/

/*=============================
           Methods
=============================*/
/** Checks if a given value exists.
 *  Parameters:
 *   value: The value to check.
 *  Returns: True if exists and false otherwise.
 */
function Exists(value) {
    return !(value == null || value == '' || value.length == null || value.length <= 0);
}