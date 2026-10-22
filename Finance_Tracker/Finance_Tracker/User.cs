using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Finance_Tracker
{
    public class User
    {
        public long Id { get; } = GenerateUniqueId();
        public int Age { get;
            set {
                if (value >= 15) {
                    field = value;
                }
            }
        }
        public string UserName { get;
            set {
                if (!string.IsNullOrWhiteSpace(value)) {
                    field = value;
                }
            }
        }
        public string Password { get;
            set {
                if (!string.IsNullOrWhiteSpace(value) && value.All(c => c >= 33 && c <= 126)) {
                    field = value;
                }
            }
        }

        private static long GenerateUniqueId() {
            byte[] bytes = Guid.NewGuid().ToByteArray();
            long id = BitConverter.ToInt64(bytes, 0) & long.MaxValue;

            return id == 0 ? 1 : id;
        }
    }
}
