# TDD Subagents System — Setup Completo

## 🎯 Resumen Ejecutivo

He creado un sistema completo de **3 subagentes especializados** para el ciclo TDD en Claude Code. Cada agente maneja UNA fase sin interferencias ni asunciones.

### Fases del TDD
1. **🔴 RED Phase** (`tdd-red-phase.agent.md`) — Escribe tests que fallan
2. **🟢 GREEN Phase** (`tdd-green-phase.agent.md`) — Implementa lógica mínima para pasar tests
3. **🔵 REFACTOR Phase** (`tdd-refactor-phase.agent.md`) — Mejora código manteniendo tests verdes

---

## 📋 Archivo Principal: `.claude/AGENTS.md`

Documento central que describe:
- La filosofía del sistema (separación estricta de fases)
- Stack tecnológico (.NET 8+, NUnit, AwesomeAssertions)
- Protocolo de comunicación con cuestionarios
- Reglas y restricciones por fase
- Cómo invocar cada subagente

**Características clave:**
- ✅ AwesomeAssertions obligatorio (NUNCA Fluent Assertions ni Assert nativo)
- ✅ Cuestionarios antes de cualquier cambio de código
- ✅ Aprobación explícita del usuario entre fases
- ✅ Sin asunciones: todo se pregunta

---

## 🤖 Subagentes

### 1. tdd-red-phase (Rojo)
**Descripción:** Escribe tests que fallan. Solo agrega stubs para compilación.

**Responsabilidades:**
- Crear test classes con AAA pattern (Arrange-Act-Assert)
- Usar AwesomeAssertions exclusivamente
- Seguir naming: `MethodUnderTest_Scenario_ExpectedBehavior`
- Agregar stubs mínimos (métodos vacíos, interfaces, clases stub)
- Confirmar test falla por la razón correcta
- Enviar cuestionario antes de cada cambio

**Restricciones:**
- ❌ NO escribir lógica de producción
- ❌ NO modificar tests existentes
- ❌ NO asumir nada

**Skills asociadas:** `testing-practices.md`

---

### 2. tdd-green-phase (Verde)
**Descripción:** Implementa lógica mínima para que tests pasen.

**Responsabilidades:**
- Escribir código de producción SIMPLE
- Hacer que TODOS los tests pasen (GREEN)
- Mantener la implementación directa (sin refactoring)
- Usar AwesomeAssertions verificando las assertions del test
- Confirmar tests pasan
- Enviar cuestionario antes de cada cambio

**Restricciones:**
- ❌ NO refactorizar
- ❌ NO modificar tests
- ❌ NO agregar lógica innecesaria
- ❌ NO aplicar patrones de diseño

**Skills asociadas:** `testing-practices.md`

---

### 3. tdd-refactor-phase (Refactor)
**Descripción:** Mejora código manteniendo tests verdes.

**Responsabilidades:**
- Aplicar SOLID principles
- Extraer métodos para reducir complejidad
- Mejorar naming y formatting
- Usar LINQ en lugar de loops
- Introducir interfaces para inyección de dependencias
- Mejorar organización de tests
- Verificar tests aún pasan
- Enviar cuestionario antes de cada cambio

**Restricciones:**
- ❌ NO cambiar comportamiento
- ❌ NO agregar tests
- ❌ NO romper tests
- ❌ NO asumir preferencias

**Skills asociadas:** `refactoring-practices.md` + `testing-practices.md`

---

## 🛠️ Skills (Conocimiento del Dominio)

### testing-practices.md
Guía completa de testing que incluye:
- **Naming Convention**: `MethodUnderTest_Scenario_ExpectedBehavior`
- **AAA Pattern**: Arrange-Act-Assert con comentarios explícitos
- **AwesomeAssertions**: Tabla completa de assertions vs nativas (PROHIBIDO usar Assert.*)
- **Test Setup**: Cuándo usar [SetUp] vs setup inline
- **Test Doubles**: Mocks, Stubs, Fakes
- **Mutation Testing**: Verificación de calidad de tests (kill rate >= 80%)
- **Test Organization**: Por feature/functionality
- **Anti-patterns**: Qué evitar

### refactoring-practices.md
Guía de refactoring .NET que incluye:
- **Naming Conventions**: PascalCase, camelCase, _camelCase, UPPER_SNAKE_CASE
- **File-Scoped Namespaces**: Estándar moderno .NET
- **Class Structure**: Orden correcto (constants, fields, properties, constructors, methods)
- **Patrones de Refactoring**: Extract method, inline, replace loop with LINQ, etc.
- **SOLID Principles**: SRP, OCP, LSP, ISP, DIP
- **Async/Await**: Patrones correctos con sufijo Async
- **Elimination of Duplication**: DRY principle
- **Code Complexity**: Guard clauses, nested reduction

---

## 💬 Protocolo de Comunicación

### Flujo de Cada Fase

```
1. Usuario invoca fase (ej: "@tdd-red-phase Crear test para Add")
   ↓
2. Subagente envía CUESTIONARIO (6 preguntas) en el MISMO MENSAJE
   ↓
3. Usuario RESPONDE el cuestionario
   ↓
4. Subagente EJECUTA cambios de código
   ↓
5. Subagente VERIFICA (tests pasan/fallan correctamente)
   ↓
6. Subagente REPORTA estado (RED/VERDE/REFACTORED)
   ↓
7. Usuario APRUEBA fase
   ↓
8. Usuario INVOCA siguiente subagente
```

### Cuestionario Obligatorio

Todos los subagentes envían cuestionarios ANTES de hacer cambios:

**Ejemplo para RED Phase:**
```
📋 RED PHASE QUESTIONS:

1. [File Location] ¿Dónde crear la test class?
2. [Test Class Name] ¿Nombre de la clase de test?
3. [Test Method Name] ¿Nombre específico del test?
4. [Input/Output Scope] ¿Inputs y outputs esperados?
5. [Stub Location] ¿Dónde ir el código stub?
6. [Edge Cases] ¿Casos límite o condiciones de error?

Por favor responda antes de crear el test.
```

---

## ⚙️ Cómo Usar el Sistema

### Paso 1: Invoca RED Phase
```
@tdd-red-phase Necesito crear un método Calculator.Add(a, b) 
que sume dos números enteros. El test debe verificar que 
Add(5, 3) retorna 8.
```

**RED responde con cuestionario → responde → RED crea test que falla ✗**

### Paso 2: Apruebas RED
```
Bien, veo que el test falla correctamente. 
¿Listo para GREEN phase?
```

### Paso 3: Invoca GREEN Phase
```
@tdd-green-phase Implementa el Add method para pasar el test RED.
```

**GREEN responde con cuestionario → responde → GREEN implementa → todos pasan ✓**

### Paso 4: Apruebas GREEN
```
Perfecto, Add ahora pasa. ¿Listo para REFACTOR?
```

### Paso 5: Invoca REFACTOR Phase
```
@tdd-refactor-phase Mejora la estructura y naming del código Calculator.
```

**REFACTOR responde con cuestionario → responde → REFACTOR mejora → todos aún pasan ✓**

### Paso 6: Apruebas REFACTOR
```
Excelente. Ciclo TDD completo para esta feature.
```

---

## 🚫 Reglas Críticas

### AwesomeAssertions: OBLIGATORIO
```csharp
// ✅ REQUERIDO
result.Should().Be(5);
items.Should().HaveCount(3);
action.Should().Throw<ArgumentException>();

// ❌ PROHIBIDO - Red flags en los agentes
Assert.Equal(5, result);                    // NUnit/xUnit nativo
result.Should().HaveCount(3);               // Así de estricto - usar HaveCount()
new FluentAssertions...                     // Fluent Assertions (no permitida)
```

### Sin Asunciones
Los agentes SIEMPRE preguntan:
- ❌ Nunca: "Asumo que quieres..."
- ✅ Siempre: "¿Quieres que...?" (en cuestionario)

### Fase Única
Cada agente SOLO hace su fase:
- RED: Tests + stubs. NO implementación.
- GREEN: Implementación. NO refactoring.
- REFACTOR: Mejora de código. NO nuevas features.

---

## 📊 Estructura de Archivos

```
.claude/
├── AGENTS.md                              # Documentación central
├── agents/
│   ├── tdd-red-phase.agent.md            # Subagente RED
│   ├── tdd-green-phase.agent.md          # Subagente GREEN
│   ├── tdd-refactor-phase.agent.md       # Subagente REFACTOR
│   ├── pairing-tdd-red.agent.md          # [Archivos legacy]
│   ├── pairing-tdd-green.agent.md        # [Archivos legacy]
│   └── pairing-tdd-refactor.agent.md     # [Archivos legacy]
└── skills/
    ├── testing-practices.md              # Skill: Testing & Assertions
    └── refactoring-practices.md          # Skill: Refactoring & .NET standards
```

---

## ✅ Checklist de Validación

- [x] 3 subagentes creados (RED, GREEN, REFACTOR)
- [x] Cada uno con responsibilities claras
- [x] Protocolo de cuestionarios implementado
- [x] AwesomeAssertions como regla obligatoria
- [x] Skill de testing con naming, AAA, assertions, mutation testing
- [x] Skill de refactoring con SOLID, naming, formatting .NET
- [x] AGENTS.md actualizado con mejores prácticas
- [x] Separación total entre fases
- [x] Aprobación explícita del usuario entre fases
- [x] Commit realizado

---

## 🎓 Referencias

- **Claude Code Docs**: https://code.claude.com/docs/es/sub-agents
- **CLAUDE.md vs agents.md**: https://www.mindstudio.ai/blog/codex-agents-md-vs-claude-code-claude-md-comparison
- **AwesomeAssertions**: Usado en place de Fluent Assertions (prohibido)
- **SOLID Principles**: Aplicables en REFACTOR phase
- **Mutation Testing**: Stryker.NET para verificar tests

---

## 📝 Próximos Pasos

1. **Usa los subagentes** en Claude Code con @mentions:
   ```
   @tdd-red-phase [tu requisito]
   @tdd-green-phase [tu requisito]
   @tdd-refactor-phase [tu requisito]
   ```

2. **Responde cuestionarios** en cada fase

3. **Aprueba fase** cuando esté completa

4. **Avanza a siguiente** fase

---

## 🎯 Beneficios

✅ **Separación clara**: Cada fase enfocada en su responsabilidad
✅ **Sin asunciones**: Todo se pregunta mediante cuestionarios
✅ **Control del usuario**: Aprobación explícita entre fases
✅ **Best practices**: Naming, AAA pattern, AwesomeAssertions, SOLID
✅ **Reproducibilidad**: Mismo sistema en todo el proyecto
✅ **Documentación**: Guías completas en skills
✅ **Calidad de tests**: Mutation testing & naming strict

¡Sistema completamente funcional y listo para usar! 🚀
