using System;
using System.Linq.Expressions;
using Machine.Specifications;
using System.Linq;
using Plumbing.Extensions;
using Plumbing.Utility;

namespace Plumbing.Core.Specs.Utility
{
    public class BaseMapperSpecs
    {
        

        public class when_mapping_a_to_b
        {
            Establish c = () =>
            {
                source = new A();
                mapper = new AtoBMapper();
            };

            Because b = () =>
                result = mapper.Map(source);

            It should_map_a_to_b = () =>
                result.Value.ShouldEqual(source);

            static AtoBMapper mapper;
            static A source;
            static B result;

            class A { }
            class B
            {
                public A Value { get; set; }
            }

            class AtoBMapper : BaseMapper<A, B>
            {
                public override Expression<Func<A, B>> MappingExpression
                {
                    get { return a => new B { Value = a }; }
                }
            }
        }        

        public class when_mapping_queryables
        {
            Establish c = () =>
            {
                source = new[] {1, 2, 3, 4, 5}.AsQueryable();
                mapper = new ExecutingMapper();
            };

            Because b = () =>
                result = source.Map(mapper);

            It should_not_execute_mapper_until_iterated = () =>
            {
                mapper.Executed.ShouldBeFalse();
                result.ToArray();
                mapper.Executed.ShouldBeTrue();
            };

            static IQueryable<int> source;
            static ExecutingMapper mapper;
            static IQueryable<string> result;

            class ExecutingMapper : BaseMapper<int, string>
            {
                public bool Executed { get; private set; }

                Func<int, string> Convert
                {
                    get
                    {
                        return i =>
                        {
                            Executed = true;
                            return i.ToString();
                        };
                    }
                }

                public override Expression<Func<int, string>> MappingExpression
                {
                    get
                    {
                        return i => Convert(i);
                    }
                }
            }
        }
    }
}