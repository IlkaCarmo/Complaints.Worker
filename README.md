⚙️ Complaints.Worker

Serviço de segundo plano responsável por consumir reclamações da fila RabbitMQ, aplicar classificação automática de texto, calcular prazos e persistir os dados no banco

📌 Objetivo
Este Worker faz parte da solução proposta para o case técnico. Ele processa reclamações de forma assíncrona, garantindo escalabilidade, rastreabilidade e separação de responsabilidades.

Funcionalidades
- Consumo de mensagens da fila "complaints-queue" via RabbitMQ
- Classificação automática de texto com base em palavras-chave
- Cálculo de prazo (Deadline) com base na data de criação
- Persistência dos dados e categorias no banco
- Registro de logs estruturados para rastreabilidade
- Suporte a múltiplas categorias por reclamação

🔗 Integrações
- RabbitMQ: Consumo de mensagens do canal digital
- Banco de dados: Armazenamento das reclamações processadas
- AWS S3: Armazenamento de anexos (se aplicável)
- Textract (via outro Worker): Processamento de documentos físicos
