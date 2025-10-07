window.mermaidInterop = {
    limparElemento: (elementId) => {
        const element = document.getElementById(elementId);
        if (element) {
            element.innerHTML = '';
        }
    },
    render: async (elementId, mermaidCode) => {
        try {
            const element = document.getElementById(elementId);
            if (!element) {
                console.error('Elemento não encontrado:', elementId);
                return false;
            }

            // Detecta tema do MudBlazor
            const isDarkMode = document.documentElement.classList.contains('mud-theme-dark') || 
                             document.body.classList.contains('mud-theme-dark');
            
            const config = {
                startOnLoad: false,
                theme: isDarkMode ? 'dark' : 'base',
                securityLevel: 'loose',
                fontFamily: 'Roboto, sans-serif',
                themeVariables: {
                    primaryColor: isDarkMode ? '#FFFFFF' : '#FFFFFF',
                    primaryTextColor: isDarkMode ? '#FFFFFF' : '#000000',
                    primaryBorderColor: isDarkMode ? '#666666' : '#000000',
                    lineColor: isDarkMode ? '#FFFFFF' : '#000000',
                    background: isDarkMode ? '#121212' : '#FFFFFF',
                    classFillColor: 'transparent'
                }
            };
            
            mermaid.initialize(config);

            // Limpa elemento antes de renderizar
            element.innerHTML = '';
            
            // Usa API moderna do Mermaid
            const { svg } = await mermaid.render(`diagram-${elementId}-${Date.now()}`, mermaidCode);
            element.innerHTML = svg;
            
            // Adiciona funcionalidade de zoom
            window.zoomManager.init(elementId);
            
            return true;
        } catch (e) {
            console.error("Mermaid render error:", e);
            const element = document.getElementById(elementId);
            if(element) {
                element.innerHTML = `<div style="padding: 1rem; border: 1px solid #f57c00; background: #fff3e0; border-radius: 4px;">
                    <strong>Erro na sintaxe Mermaid:</strong><br/>
                    ${e.message || 'Erro desconhecido'}
                </div>`;
            }
            return false;
        }
    },
    saveToLocalStorage: (key, value) => {
        try {
            localStorage.setItem(key, value);
            return true;
        } catch (e) {
            console.error('Erro ao salvar no localStorage:', e);
            return false;
        }
    },
    readFromLocalStorage: (key) => {
        try {
            return localStorage.getItem(key);
        } catch (e) {
            console.error('Erro ao ler do localStorage:', e);
            return null;
        }
    }
};

window.resizeManager = {
    dotNetRef: null,

    start: function (dotNetReference) {
        this.dotNetRef = dotNetReference;
        document.addEventListener('mousemove', this.handleMouseMove);
        document.addEventListener('mouseup', this.handleMouseUp);
        document.body.style.cursor = 'col-resize';
        document.body.style.userSelect = 'none';
    },

    handleMouseMove: (e) => {
        if (!window.resizeManager.dotNetRef) return;

        const container = document.getElementById('container');
        if (!container) return;

        const rect = container.getBoundingClientRect();
        if (rect.width === 0) return;

        const percentage = ((e.clientX - rect.left) / rect.width) * 100;
        window.resizeManager.dotNetRef.invokeMethodAsync('UpdateWidth', percentage);
    },

    handleMouseUp: () => {
        window.resizeManager.dispose();
    },
    
    dispose: () => {
        if (window.resizeManager.dotNetRef) {
            document.removeEventListener('mousemove', window.resizeManager.handleMouseMove);
            document.removeEventListener('mouseup', window.resizeManager.handleMouseUp);
            window.resizeManager.dotNetRef = null;
            document.body.style.cursor = '';
            document.body.style.userSelect = '';
        }
    }
};

// Função para inserir TAB no textarea
window.insertTab = (elementId) => {
    const element = document.getElementById(elementId);
    if (element) {
        const start = element.selectionStart;
        const end = element.selectionEnd;
        const value = element.value;
        
        element.value = value.substring(0, start) + '    ' + value.substring(end);
        element.selectionStart = element.selectionEnd = start + 4;
        
        // Dispara evento de input para atualizar o Blazor
        element.dispatchEvent(new Event('input', { bubbles: true }));
    }
};

// Adiciona listener para prevenir TAB padrão
document.addEventListener('DOMContentLoaded', () => {
    document.addEventListener('keydown', (e) => {
        if (e.key === 'Tab' && e.target.id && e.target.id.startsWith('editor-')) {
            e.preventDefault();
            insertTab(e.target.id);
        }
    });
});

// Gerenciador de Zoom
window.zoomManager = {
    init: (elementId) => {
        const element = document.getElementById(elementId);
        if (!element) return;
        
        const svg = element.querySelector('svg');
        if (!svg) return;
        
        let scale = 1;
        let isDragging = false;
        let startX, startY, translateX = 0, translateY = 0;
        
        // Aplica transformação
        const applyTransform = () => {
            svg.style.transform = `translate(${translateX}px, ${translateY}px) scale(${scale})`;
            svg.style.transformOrigin = 'center';
        };
        
        // Zoom com scroll do mouse
        element.addEventListener('wheel', (e) => {
            e.preventDefault();
            const delta = e.deltaY > 0 ? 0.9 : 1.1;
            scale = Math.max(0.1, Math.min(3, scale * delta));
            applyTransform();
        });
        
        // Pan com arrastar
        svg.addEventListener('mousedown', (e) => {
            isDragging = true;
            startX = e.clientX - translateX;
            startY = e.clientY - translateY;
            svg.style.cursor = 'grabbing';
        });
        
        document.addEventListener('mousemove', (e) => {
            if (!isDragging) return;
            translateX = e.clientX - startX;
            translateY = e.clientY - startY;
            applyTransform();
        });
        
        document.addEventListener('mouseup', () => {
            isDragging = false;
            svg.style.cursor = 'grab';
        });
        
        // Estilo inicial
        svg.style.cursor = 'grab';
        svg.style.transition = 'transform 0.1s ease';
    },
    
    setZoom: (elementId, zoomLevel) => {
        const element = document.getElementById(elementId);
        if (!element) return;
        
        const svg = element.querySelector('svg');
        if (!svg) return;
        
        const scale = Math.max(0.1, Math.min(3, zoomLevel));
        svg.style.transform = `scale(${scale})`;
        svg.style.transformOrigin = 'center';
    }
};

// Exportador de PDF
window.exportToPdf = async (elementId) => {
    const element = document.getElementById(elementId);
    if (!element) return;
    
    const svg = element.querySelector('svg');
    if (!svg) return;
    
    try {
        // Verifica se jsPDF está disponível
        if (!window.jspdf) {
            throw new Error('jsPDF não carregado');
        }
        
        // Clona o SVG completo
        const svgClone = svg.cloneNode(true);
        svgClone.style.transform = 'none';
        
        // Obtém dimensões reais do SVG
        const rect = svg.getBoundingClientRect();
        const width = rect.width || 800;
        const height = rect.height || 600;
        
        // Serializa o SVG completo com todos os atributos
        let svgString = new XMLSerializer().serializeToString(svgClone);
        
        // Adiciona namespace se não existir
        if (!svgString.includes('xmlns')) {
            svgString = svgString.replace('<svg', '<svg xmlns="http://www.w3.org/2000/svg"');
        }
        
        // Adiciona fundo branco
        svgString = svgString.replace('>', `><rect width="100%" height="100%" fill="white"/>`);
        
        // Escala para melhor qualidade
        const scale = 2;
        const canvas = document.createElement('canvas');
        const ctx = canvas.getContext('2d');
        canvas.width = width * scale;
        canvas.height = height * scale;
        
        // Cria data URL do SVG
        const svgDataUrl = 'data:image/svg+xml;charset=utf-8,' + encodeURIComponent(svgString);
        
        const img = new Image();
        img.onload = () => {
            try {
                // Desenha com escala para melhor qualidade
                ctx.scale(scale, scale);
                ctx.drawImage(img, 0, 0, width, height);
                
                // Cria PDF
                const { jsPDF } = window.jspdf;
                const pdf = new jsPDF({
                    orientation: width > height ? 'landscape' : 'portrait',
                    unit: 'pt',
                    format: [width, height]
                });
                
                const imgData = canvas.toDataURL('image/png', 1.0);
                pdf.addImage(imgData, 'PNG', 0, 0, width, height);
                pdf.save('diagrama-classe.pdf');
                
            } catch (pdfError) {
                console.error('Erro ao gerar PDF:', pdfError);
                alert('Erro ao gerar PDF. Tente exportar como SVG.');
            }
        };
        
        img.onerror = () => {
            console.error('Erro ao carregar SVG');
            alert('Erro ao carregar diagrama. Tente exportar como SVG.');
        };
        
        img.src = svgDataUrl;
        
    } catch (error) {
        console.error('Erro ao exportar PDF:', error);
        alert('Erro ao exportar PDF. Tente exportar como SVG.');
    }
};

// Fallback: exportar como SVG
window.exportToSvg = (elementId) => {
    const element = document.getElementById(elementId);
    if (!element) return;
    
    const svg = element.querySelector('svg');
    if (!svg) return;
    
    const svgData = new XMLSerializer().serializeToString(svg);
    const svgBlob = new Blob([svgData], { type: 'image/svg+xml;charset=utf-8' });
    const url = URL.createObjectURL(svgBlob);
    
    const a = document.createElement('a');
    a.href = url;
    a.download = 'diagrama-classe.svg';
    a.click();
    
    URL.revokeObjectURL(url);
};

// Salvar arquivo .dcl com seleção de pasta
window.salvarArquivo = async (content, filename) => {
    try {
        // Verifica se a API File System Access está disponível
        if ('showSaveFilePicker' in window) {
            const options = {
                suggestedName: filename.endsWith('.dcl') ? filename : filename + '.dcl',
                types: [{
                    description: 'Arquivo de Diagrama',
                    accept: { 'text/plain': ['.dcl'] }
                }]
            };
            
            // Tenta usar o último diretório salvo
            const lastHandle = await window.mermaidInterop.readFromLocalStorage('lastDirectoryHandle');
            if (lastHandle) {
                options.startIn = lastHandle;
            }
            
            const handle = await window.showSaveFilePicker(options);
            const writable = await handle.createWritable();
            await writable.write(content);
            await writable.close();
            
            // Salva referência do diretório (não funciona diretamente, mas tenta)
            try {
                const dirHandle = await handle.getParent?.();
                if (dirHandle) {
                    await window.mermaidInterop.saveToLocalStorage('lastDirectoryHandle', dirHandle.name);
                }
            } catch (e) {
                // Ignora erro ao salvar diretório
            }
            
            return true;
        } else {
            // Fallback para navegadores antigos
            const blob = new Blob([content], { type: 'text/plain;charset=utf-8' });
            const url = URL.createObjectURL(blob);
            const a = document.createElement('a');
            a.href = url;
            a.download = filename.endsWith('.dcl') ? filename : filename + '.dcl';
            a.click();
            URL.revokeObjectURL(url);
            return true;
        }
    } catch (e) {
        if (e.name !== 'AbortError') {
            console.error('Erro ao salvar arquivo:', e);
            return false;
        }
        return false;
    }
};

// Abrir arquivo .dcl
window.abrirArquivo = async () => {
    try {
        // Verifica se a API File System Access está disponível
        if ('showOpenFilePicker' in window) {
            const options = {
                types: [{
                    description: 'Arquivo de Diagrama',
                    accept: { 'text/plain': ['.dcl'] }
                }],
                multiple: false
            };
            
            const [handle] = await window.showOpenFilePicker(options);
            const file = await handle.getFile();
            const content = await file.text();
            return content;
        } else {
            // Fallback para navegadores antigos
            return new Promise((resolve) => {
                const input = document.createElement('input');
                input.type = 'file';
                input.accept = '.dcl';
                
                input.onchange = (e) => {
                    const file = e.target.files[0];
                    if (file) {
                        const reader = new FileReader();
                        reader.onload = (event) => {
                            resolve(event.target.result);
                        };
                        reader.readAsText(file);
                    } else {
                        resolve(null);
                    }
                };
                
                input.click();
            });
        }
    } catch (e) {
        if (e.name !== 'AbortError') {
            console.error('Erro ao abrir arquivo:', e);
        }
        return null;
    }
};

