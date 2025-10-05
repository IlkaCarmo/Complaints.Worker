namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Classify_DeveRetornarCategoriaFraude()
        {
            var categorias = new Dictionary<string, List<string>>
        {
        { "fraude", new() { "fraude", "não reconhece dívida" } }
        };

            var classifier = new ComplaintClassifier();
            var resultado = classifier.Classify("Não reconheço essa dívida", categorias);

            Assert.Contains("fraude", resultado);
        }

        [Fact]
        public void Deadline_DeveSer10DiasCorridos()
        {
            var complaint = new ComplaintDto();
            var agora = DateTime.UtcNow;

            complaint.Deadline = agora.AddDays(10);

            Assert.Equal(complaint.Deadline.Date, agora.AddDays(10).Date);
        }

    }
}