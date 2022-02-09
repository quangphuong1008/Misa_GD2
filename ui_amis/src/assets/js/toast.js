/**
 * Class hiển thị thông báo
 * Author: Do Huu Toan
 */

class Toast {
    /**
     * 
     * @param {element} mainElement: Element muốn hiển thị toast vào trong 
     * @param {string} tittle : Nội dung của toast
     */
    constructor(mainElement, tittle, type = 'success') {
        this.mainElement = mainElement;
        this.tittle = tittle;

        const toast = document.createElement('div');
        toast.classList.add("toast-messenger", "toast-messenger__success")
        if(type  == 'success'){
            toast.innerHTML = `<i class="check-icon"></i>
            <p>${this.tittle}</p>
            <i class="delete-icon"></i>`
        }
        else if (type == 'error'){
            toast.innerHTML = `<i class="error-icon"></i>
            <p>${this.tittle}</p>
            <i class="delete-icon"></i>`
        }
        mainElement.appendChild(toast);

        setTimeout(function(){
            mainElement.removeChild(toast);
        }, 3000+1000);
    }

}

export default Toast;