using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

/// <summary>
/// API Controller responsável pelo gerenciamento de operações de vendas,
/// incluindo criação, consulta, atualização, cancelamento e exclusão de vendas e itens de venda.
/// Utiliza o padrão CQRS com MediatR para orquestrar as requisições.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SalesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="SalesController"/>.
    /// </summary>
    /// <param name="mediator">Instância do MediatR para envio de comandos e consultas.</param>
    /// <param name="mapper">Instância do AutoMapper para mapeamento de objetos.</param>
    public SalesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Cria uma nova venda.
    /// </summary>
    /// <param name="request">Dados da venda a ser criada.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação.</param>
    /// <returns>Retorna os detalhes da venda criada.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = new CreateSaleCommand(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
        {
            Success = true,
            Message = "Sale created successfully",
            Data = response
        });
    }

    /// <summary>
    /// Obtém os detalhes de uma venda pelo seu identificador.
    /// </summary>
    /// <param name="id">Identificador único da venda.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação.</param>
    /// <returns>Retorna os detalhes da venda, se encontrada.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSale([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var validator = new GetSaleRequestValidator();
        var request = new GetSaleRequest { Id = id };

        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = new GetSaleCommand(request);
        var response = await _mediator.Send(command, cancellationToken);

        if (response == null)
            return NotFound(new ApiResponse { Success = false, Message = "Sale not found" });

        return Ok(new ApiResponseWithData<GetSaleResponse>
        {
            Success = true,
            Message = "Sale retrieved successfully",
            Data = response
        });
    }

    /// <summary>
    /// Atualiza os dados de uma venda existente.
    /// </summary>
    /// <param name="id">Identificador único da venda.</param>
    /// <param name="request">Dados atualizados da venda.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação.</param>
    /// <returns>Retorna os dados da venda atualizada.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<Application.Sales.UpdateSale.UpdateSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSale([FromRoute] Guid id, [FromBody] Application.Sales.UpdateSale.UpdateSaleRequest request, CancellationToken cancellationToken)
    {
        if (id != request.Id)
            return BadRequest(new ApiResponse { Success = false, Message = "Id mismatch" });

        var validator = new Application.Sales.UpdateSale.UpdateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = new Application.Sales.UpdateSale.UpdateSaleCommand(request);
        var response = await _mediator.Send(command, cancellationToken);

        if (response == null)
            return NotFound(new ApiResponse { Success = false, Message = "Sale not found" });

        return Ok(new ApiResponseWithData<Application.Sales.UpdateSale.UpdateSaleResponse>
        {
            Success = true,
            Message = "Sale updated successfully",
            Data = response
        });
    }

    /// <summary>
    /// Cancela uma venda existente.
    /// </summary>
    /// <param name="id">Identificador único da venda.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação.</param>
    /// <returns>Retorna sucesso ou falha no cancelamento da venda.</returns>
    [HttpPost("{id}/cancel")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelSale([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new Application.Sales.CancelSale.CancelSaleRequest { Id = id };
        var validator = new Application.Sales.CancelSale.CancelSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = new Application.Sales.CancelSale.CancelSaleCommand(request);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result)
            return NotFound(new ApiResponse { Success = false, Message = "Sale not found" });

        return Ok(new ApiResponse { Success = true, Message = "Sale cancelled successfully" });
    }

    /// <summary>
    /// Cancela um item específico de uma venda.
    /// </summary>
    /// <param name="saleId">Identificador único da venda.</param>
    /// <param name="itemId">Identificador único do item.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação.</param>
    /// <returns>Retorna sucesso ou falha no cancelamento do item.</returns>
    [HttpPost("{saleId}/items/{itemId}/cancel")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelSaleItem([FromRoute] Guid saleId, [FromRoute] Guid itemId, CancellationToken cancellationToken)
    {
        var request = new Application.Sales.CancelSaleItem.CancelSaleItemRequest { SaleId = saleId, ItemId = itemId };
        var validator = new Application.Sales.CancelSaleItem.CancelSaleItemRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = new Application.Sales.CancelSaleItem.CancelSaleItemCommand(request);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result)
            return NotFound(new ApiResponse { Success = false, Message = "Sale or item not found" });

        return Ok(new ApiResponse { Success = true, Message = "Sale item cancelled successfully" });
    }

    /// <summary>
    /// Exclui uma venda existente.
    /// </summary>
    /// <param name="id">Identificador único da venda.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação.</param>
    /// <returns>Retorna sucesso ou falha na exclusão da venda.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSale([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteSale.DeleteSaleRequest { Id = id };
        var validator = new DeleteSale.DeleteSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = new Application.Sales.DeleteSale.DeleteSaleCommand(id);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result)
            return NotFound(new ApiResponse { Success = false, Message = "Sale not found" });

        return Ok(new ApiResponse { Success = true, Message = "Sale deleted successfully" });
    }
}
