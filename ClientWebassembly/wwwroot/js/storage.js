window.storage = {
    set: function (key, value) {
        try {
            localStorage.setItem(key, value);
        } catch (e) {
            console.error('localStorage.set failed', e);
        }
    },
    get: function (key) {
        try {
            return localStorage.getItem(key);
        } catch (e) {
            console.error('localStorage.get failed', e);
            return null;
        }
    },
    remove: function (key) {
        try {
            localStorage.removeItem(key);
        } catch (e) {
            console.error('localStorage.remove failed', e);
        }
    }
};