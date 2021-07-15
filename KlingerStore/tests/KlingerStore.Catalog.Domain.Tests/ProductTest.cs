using KlingerStore.Catalog.Domain.Class;
using KlingerStore.Core.Domain.DomainObjects;
using System;
using Xunit;

namespace KlingerStore.Catalog.Domain.Tests
{
    public class ProductTest
    {
        [Fact]
        public void Produto_Validar_ValidacoesDevemRetornarException()
        {
            var ex = Assert.Throws<DomainException>(() =>
                new Product(Guid.NewGuid(), string.Empty, "Descricao", false, 100, DateTime.Now, "Imagem", new Dimensions(1, 1, 1))
            );
            Assert.Equal("O campo Nome do produto não pode estar vazio", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Product(Guid.NewGuid(), "Nome", string.Empty, false, 100, DateTime.Now, "Imagem", new Dimensions(1, 1, 1))
            );
            Assert.Equal("O campo Descricao do produto não pode estar vazio", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Product(Guid.NewGuid(), "Nome", "Descricao", false, 0, DateTime.Now, "Imagem", new Dimensions(1, 1, 1))
            );
            Assert.Equal("O campo Valor do produto não pode se menor igual a 0", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Product(Guid.Empty, "Nome", "Descricao", false, 100, DateTime.Now, "Imagem", new Dimensions(1, 1, 1))
            );
            Assert.Equal("O campo CategoriaId do produto não pode estar vazio", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Product(Guid.NewGuid(), "Nome", "Descricao", false, 100, DateTime.Now, string.Empty, new Dimensions(1, 1, 1))
            );
            Assert.Equal("O campo Imagem do produto não pode estar vazio", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Product(Guid.NewGuid(), "Nome", "Descricao", false, 100, DateTime.Now, "Imagem", new Dimensions(0, 1, 1))
            );
            Assert.Equal("O campo Altura não pode ser menor ou igual a 0", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Product(Guid.NewGuid(), "Nome", "Descricao", false, 100, DateTime.Now, "Imagem", new Dimensions(1, 0, 1))
            );
            Assert.Equal("O campo Largura não pode ser menor ou igual a 0", ex.Message);
        }
    }
}
