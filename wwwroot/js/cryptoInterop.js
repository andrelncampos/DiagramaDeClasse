window.cryptoInterop = {
    async encrypt(text) {
        const encoder = new TextEncoder();
        const data = encoder.encode(text);
        
        // Gera chave baseada no navegador (fingerprint simples)
        const keyMaterial = await this.getKeyMaterial();
        const key = await window.crypto.subtle.deriveKey(
            { name: "PBKDF2", salt: encoder.encode("DiagramaDeClasse"), iterations: 100000, hash: "SHA-256" },
            keyMaterial,
            { name: "AES-GCM", length: 256 },
            false,
            ["encrypt"]
        );
        
        const iv = window.crypto.getRandomValues(new Uint8Array(12));
        const encrypted = await window.crypto.subtle.encrypt(
            { name: "AES-GCM", iv: iv },
            key,
            data
        );
        
        // Combina IV + dados criptografados
        const combined = new Uint8Array(iv.length + encrypted.byteLength);
        combined.set(iv);
        combined.set(new Uint8Array(encrypted), iv.length);
        
        return btoa(String.fromCharCode(...combined));
    },
    
    async decrypt(encryptedText) {
        try {
            const combined = Uint8Array.from(atob(encryptedText), c => c.charCodeAt(0));
            const iv = combined.slice(0, 12);
            const data = combined.slice(12);
            
            const keyMaterial = await this.getKeyMaterial();
            const encoder = new TextEncoder();
            const key = await window.crypto.subtle.deriveKey(
                { name: "PBKDF2", salt: encoder.encode("DiagramaDeClasse"), iterations: 100000, hash: "SHA-256" },
                keyMaterial,
                { name: "AES-GCM", length: 256 },
                false,
                ["decrypt"]
            );
            
            const decrypted = await window.crypto.subtle.decrypt(
                { name: "AES-GCM", iv: iv },
                key,
                data
            );
            
            const decoder = new TextDecoder();
            return decoder.decode(decrypted);
        } catch (e) {
            console.error("Erro ao descriptografar:", e);
            return null;
        }
    },
    
    async getKeyMaterial() {
        // Usa identificador único do navegador como base para a chave
        const fingerprint = navigator.userAgent + navigator.language + screen.width + screen.height;
        const encoder = new TextEncoder();
        return await window.crypto.subtle.importKey(
            "raw",
            encoder.encode(fingerprint),
            { name: "PBKDF2" },
            false,
            ["deriveKey"]
        );
    }
};
